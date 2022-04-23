import cv2
import numpy as np

from cv04.cv04 import img_read
from cv07.cv07 import find_shapes, find_mass_center
from cv3.cv3 import show_img


def process_img(img):
    kernel = np.ones((3, 3), np.uint8)
    img_open = transformation(np.copy(img) * 1.0, np.array([0, -20, 0]), True)
    c = img - np.abs(img - img_open)
    c[c < 110] = 0

    c = cv2.dilate(cv2.erode(c, kernel), kernel)
    c[c > 0] = 1
    c = cv2.dilate(cv2.erode(c, kernel), kernel)

    c, seg = find_shapes(c)
    c = find_mass_center(c, seg, cv2.cvtColor(img, cv2.COLOR_GRAY2RGB), lambda x: 1 if x > 10 else 0)
    return c


def dilate(img, element):
    c = np.copy(img)

    for x in range(img.shape[0]):
        for y in range(img.shape[1]):
            try:
                c[x, y] = np.max(img[x:x + element.shape[0], y] + element)
            except Exception:
                pass

    return c


def erode(img, element):
    c = np.copy(img)

    for x in range(img.shape[0]):
        for y in range(img.shape[1]):
            try:
                c[x, y] = np.min(img[x:x + element.shape[0], y] - element)
            except Exception:
                pass

    return c


def transformation(img, element, o=False):
    if o:
        return erode(dilate(img, element), element)
    return dilate(erode(img, element), element)


def main():
    element = np.array([1, 2, 1])
    # show_img(transformation(img_read("cv09_bunkyB.bmp", cv2.COLOR_BGR2GRAY), element), "bunkyB", cmap="gray")
    # show_img(transformation(img_read("cv09_bunkyC.bmp", cv2.COLOR_BGR2GRAY), element, True), "bunkyC", cmap="gray")
    show_img(process_img(img_read("cv09_rice.bmp", cv2.COLOR_BGR2GRAY)), "rice")


if __name__ == "__main__":
    main()
