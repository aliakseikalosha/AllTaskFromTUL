import cv2
import numpy as np
import matplotlib.pyplot as plt


def read_img(path):
    return cv2.cvtColor(cv2.imread(path), cv2.COLOR_BGR2RGB)


def show_hist(img, func=None):
    color = ('r', 'g', 'b')
    for i, col in enumerate(color):
        h = cv2.calcHist([img], [i], None, [256], [0, 256])
        if func is not None:
            h = func(h)
        plt.plot(h, color=col)
        plt.xlim([0, 256])
    plt.show()


def show_hist_smooth(img, k=2):
    def smooth(arr):
        for i in range(k, len(arr) - k):
            arr[i] = sum(arr[i - k: i + k]) / (2 * k + 1)
        return arr

    show_hist(img, smooth)


def main():
    img = read_img('cv01_obr.bmp')
    show_hist(img)
    show_hist_smooth(img)


if __name__ == "__main__":
    main()
