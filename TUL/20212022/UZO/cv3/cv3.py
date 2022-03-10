import math

from cv1.cv1 import read_img
import numpy as np
import matplotlib.pyplot as plt


def show_img(img):
    plt.imshow(img)
    plt.show()


def enlarge_img(img, a, b):
    new_shape = img.shape
    new_shape = (math.floor(new_shape[0] * a), math.floor(new_shape[1] * b), new_shape[2])
    new_img = np.zeros(new_shape)
    for x in range(0, new_shape[0]):
        for y in range(0, new_shape[1]):
            new_img[x, y, :] = img[math.floor(x / a), math.floor(y / b), :] / 255

    show_img(new_img)


def bevel_img(img, fi):
    new_shape = img.shape
    new_shape = (math.floor(new_shape[0] + new_shape[1] * math.tan(fi)), new_shape[1], new_shape[2])
    new_img = np.zeros(new_shape)
    for x in range(0, new_shape[0]):
        for y in range(0, new_shape[1]):
            old_x = math.floor(x - y * math.tan(fi))
            if 0 < old_x < img.shape[0]:
                new_img[x, y, :] = img[old_x, y, :] / 255
            else:
                new_img[x, y, :] = np.ones((1, new_shape[2]))

    show_img(new_img)


def main():
    img = read_img('cv03_robot.bmp')
    enlarge_img(img, 1.2, 1)
    bevel_img(img, np.pi * 75/180)


if __name__ == "__main__":
    main()
