import cv2 as cv
import matplotlib.pyplot as plt
import numpy as np

from cv1.cv1 import calc_hist
from cv3.cv3 import enlarge_img


def find_on_image(img, target):
    (width, height, _) = target.shape
    i = cv.cvtColor(img, cv.COLOR_RGB2HSV)
    t = cv.cvtColor(target, cv.COLOR_RGB2HSV)
    min_diff = 256 * height * width
    min_x, min_y = 0, 0
    layer = 1
    step = 10
    t_h = calc_hist(t, layer)
    plt.show()
    print("size", i.shape)
    d = np.zeros((int(i.shape[0] / step) + 1, int(i.shape[1] / step) + 1, 1))
    for x in range(0, img.shape[0], step):
        for y in range(0, img.shape[1], step):
            h = calc_hist(i[x:x + width, y:y + height, :], layer)
            diff = np.sum(np.abs(t_h - h))
            d[int(x / step), int(y / step), :] = diff
            if diff < min_diff:
                min_diff = diff
                min_x, min_y = x, y
    print(min_x, min_y, min_x + width, min_y + height)
    cv.imshow('Difference', enlarge_img(np.ones(d.shape) - d / np.max(d), step, step, False))
    return min_y, min_x, min_y + height, min_x + width
