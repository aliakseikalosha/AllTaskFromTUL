import cv2
import numpy as np
from matplotlib import pyplot as plt
from mpl_toolkits.axes_grid1 import make_axes_locatable

from cv04.cv04 import img_read


def pixel_processor(img, pixel_process, shape=None):
    if shape is None:
        shape = img.shape

    processed = np.zeros(shape)
    for x in range(shape[0]):
        for y in range(shape[1]):
            processed[x, y] = pixel_process(x, y, shape)
    return processed


def laplace_detector(img):
    mat = [[0, 1, 0], [1, -4, 1], [0, 1, 0]]

    def process(x, y, shape):
        if x < 1 or y < 1 or y >= shape[1] - 1 or x >= shape[0] - 1:
            return 0
        return np.sum(mat * img[x - 1:x + 2, y - 1: y + 2])

    return pixel_processor(img, process)


def create_set_of_mat(mat0, mat45):
    mats = []
    m0 = mat0
    m45 = mat45
    for i in range(4):
        m0 = np.rot90(m0)
        m45 = np.rot90(m45)
        mats.append(m0)
        mats.append(m45)
    return mats


def set_detector(img, set_m):
    def process(x, y, shape):
        if x < 1 or y < 1 or y >= shape[1] - 1 or x >= shape[0] - 1:
            return 0
        max_v = 0
        for m in set_m:
            g = np.sum(m * img[x - 1:x + 2, y - 1: y + 2])
            if g > max_v:
                max_v = g
        return max_v

    return pixel_processor(img, process)


def sobel_detector(img):
    set_m = create_set_of_mat([[1, 2, 1], [0, 0, 0], [-1, -2, -1]], [[0, 1, 2], [-1, 0, 1], [-2, -1, 0]])
    return set_detector(img, set_m)


def kirsch_detector(img):
    set_m = create_set_of_mat([[3, 3, 3], [3, 0, 3], [-5, -5, -5]], [[3, 3, 3], [-5, 0, 3], [-5, -5, 3]])
    return set_detector(img, set_m)


def calc_spec(img):
    spec = np.fft.fftshift(np.fft.fft2(img))
    return np.log(np.abs(spec))


def show_img_colorbar(ax, img):
    im = ax.imshow(img, cmap='jet')
    divider = make_axes_locatable(ax)
    cax = divider.append_axes("right", size="5%", pad=0.05)

    plt.colorbar(im, cax=cax)


def show_result(img, edge):
    fig, ax = plt.subplots(2, 2)
    ax[0, 0].imshow(img, cmap='gray')
    show_img_colorbar(ax[1, 0], edge)
    show_img_colorbar(ax[0, 1], calc_spec(img))
    show_img_colorbar(ax[1, 1], calc_spec(edge))
    plt.show()


def main():
    img = cv2.cvtColor(img_read("cv06_robotC.bmp"), cv2.COLOR_RGB2GRAY)
    show_result(img, laplace_detector(img))
    show_result(img, sobel_detector(img))
    show_result(img, kirsch_detector(img))


if __name__ == "__main__":
    main()
