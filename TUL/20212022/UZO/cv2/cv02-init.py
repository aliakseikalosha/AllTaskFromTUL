import os

import numpy as np
import matplotlib.pyplot as plt
import cv2

from cv02 import find_on_image

plt.ion()
plt.close('all')

cap = cv2.VideoCapture('cv02_hrnecek.mp4')
target = cv2.cvtColor(cv2.imread('cv02_vzor_hrnecek.bmp'), cv2.COLOR_BGR2RGB)
while True:
    ret, bgr = cap.read()
    if not ret:
        break
    # hsv = cv2.cvtColor(bgr, cv2.COLOR_RGB2HSV)
    # hist, b = np.histogram(hsv[:,:,0], 256, (0, 256))
    (x1, y1, x2, y2) = find_on_image(cv2.cvtColor(bgr, cv2.COLOR_BGR2RGB), target)
    cv2.rectangle(bgr, (x1, y1), (x2, y2), (0, 255, 0))
    cv2.imshow('Image', bgr)
    key = 0xFF & cv2.waitKey(30)
    if key == 27:
        break

cv2.destroyAllWindows()
