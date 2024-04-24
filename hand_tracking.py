import cv2
import mediapipe as mp
import socket
import struct

# Initialize MediaPipe Hands.
mp_hands = mp.solutions.hands
hands = mp_hands.Hands(min_detection_confidence=0.5, min_tracking_confidence=0.5)

# Setup UDP for sending data to Unity.
udp_ip = '127.0.0.1'  # Localhost
udp_port = 8051  # The port where Unity will listen.
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)


cap = cv2.VideoCapture(0)

while cap.isOpened():
    ret, frame = cap.read()
    if not ret:
        break

    
    image = frame 
    
    
    results = hands.process(image)

    # If landmarks are found, send them over UDP.
    if results.multi_hand_landmarks:
        for hand_landmarks in results.multi_hand_landmarks:
            
            for idx, landmark in enumerate(hand_landmarks.landmark):
                # Creating a byte packet to send (we're sending x and y for each landmark)
                # The format 'ff' means we are packing two floats into the packet.
                landmark_data = struct.pack('ff', landmark.x, landmark.y)
                # Send the packed data through UDP
                sock.sendto(landmark_data, (udp_ip, udp_port))

    
    if results.multi_hand_landmarks:
        for hand_landmarks in results.multi_hand_landmarks:
            mp.solutions.drawing_utils.draw_landmarks(
                image, hand_landmarks, mp.solutions.hands.HAND_CONNECTIONS)
        print("Landmarks detected")
    else:
        print("No landmarks detected")

    cv2.imshow('MediaPipe Hands', cv2.flip(image, 1))
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break


cap.release()
cv2.destroyAllWindows()
