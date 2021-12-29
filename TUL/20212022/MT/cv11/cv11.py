import time

import cv2
from matplotlib import pyplot as plt
import numpy as np


def read_img(path):
    img = cv2.cvtColor(cv2.imread(path), cv2.COLOR_BGR2GRAY)
    return img


def img_with_text(img, text):
    img_text = np.copy(img)
    cv2.putText(img_text, text, (0, 260), cv2.FONT_HERSHEY_SIMPLEX, 3, 128)
    return img_text


def count_circle(img):
    circles = cv2.HoughCircles(image=img,
                               method=cv2.HOUGH_GRADIENT,
                               dp=1,
                               minDist=72,
                               param1=18,
                               param2=16,
                               minRadius=0, maxRadius=0)
    return circles.shape[1]


def task_count_circle():
    for i in range(1, 7):
        img = read_img("Cv11_c0" + str(i) + ".bmp")
        number = count_circle(img)
        plt.imshow(img_with_text(img, str(number)), cmap="gray")
        plt.show()


def count_erode(img, kernel):
    count = 0
    img = np.copy(img)

    posx = []
    posy = []
    while np.sum(img) > 0:
        count += 1
        img = cv2.erode(img, kernel, iterations=1)
        if np.sum(img) < np.sum(kernel):
            for x in range(0, img.shape[0]):
                for y in range(0, img.shape[1]):
                    posy.append(y)
                    posx.append(x)
            break

    return count, np.sum(posx)//len(posx), np.sum(posy)//len(posy)


def task_erode():
    img = read_img("Cv11_merkers.bmp") // 255
    height, width = img.shape

    height_cutoff = height // 2
    kernel = np.ones((3, 3), np.uint8)
    top_count = count_erode(img[:height_cutoff, :], kernel)
    bottom_count = count_erode(img[height_cutoff:, :], kernel)

    print(top_count, bottom_count)


def main():
    # task_count_circle()
    task_erode()


if __name__ == "__main__":
    main()
