import math

import cv2
import numpy as np
from matplotlib import pyplot as plt
from mpl_toolkits.axes_grid1 import make_axes_locatable

from cv04.cv04 import img_read


def show_spectrum(img):
    spec = np.fft.fftshift(np.fft.fft2(img))
    spec_log = np.log(np.abs(spec))
    fig, ax = plt.subplots(1, 2)
    ax[0].imshow(img, cmap='gray')
    im = ax[1].imshow(spec_log)

    divider = make_axes_locatable(ax[1])
    cax = divider.append_axes("right", size="5%", pad=0.05)

    plt.colorbar(im, cax=cax)
    plt.show()


def img_noise(img, process):
    shape = img.shape
    n = np.ones((shape[0], shape[1], 1))
    for x in range(1, shape[0]):
        for y in range(1, shape[1]):
            n[x, y] = process(x, y, shape)

    return n


def img_noise_avg(img):
    def process(x, y, shape):
        return np.sum(img[x - 1:x + 2, y - 1:y + 2]) / 9

    return img_noise(img, process)


def img_noise_rot_mask(img):
    m = []
    size = 3
    for x in range(size):
        for y in range(size):
            if x != int(size / 2) or y != int(size / 2):
                m.append((x - 1, y - 1))

    def process(x, y, shape):
        min_v = None
        min_index = (0, 0)
        for index in m:
            x_start = x + index[0] - 1
            x_end = x + index[0] + 2

            y_start = y + index[1] - 1
            y_end = y + index[1] + 2

            if x_start > -1 and x_end < shape[0] and y_start > -1 and y_end < shape[1]:
                sector = img[x_start:x_end, y_start:y_end]
                v = np.std(sector)
                if min_v is None or (min_v > v and not math.isnan(v)):
                    min_v = v
                    min_index = index

        if min_v is None:
            return img[x, y]

        color = np.sum(img[x + min_index[0] - 1:x + min_index[0] + 2, y + min_index[1] - 1:y + min_index[1] + 2]) / 9
        return color

    return img_noise(img, process)


def img_noise_median(img):
    def process(x, y, shape):
        m = list([img[x, y]])
        for i in [-2, -1, 1, 2]:
            m.append(img[x + i if -1 < x + i < shape[0] else x, y])
            m.append(img[x, y + i if -1 < y + i < shape[1] else y])
        m = sorted(m)
        return m[int(len(m) / 2)]

    return img_noise(img, process)


def process_img(path):
    img = cv2.cvtColor(img_read(path), cv2.COLOR_RGB2GRAY)
    show_spectrum(img)
    show_spectrum(img_noise_avg(img))
    show_spectrum(img_noise_rot_mask(img))
    show_spectrum(img_noise_median(img))


def main():
    show_spectrum(img_noise_rot_mask(cv2.cvtColor(img_read("cv05_robotS.bmp"), cv2.COLOR_RGB2GRAY)))
    #process_img("cv05_robotS.bmp")
    #process_img("cv05_PSS.bmp")


if __name__ == "__main__":
    main()
