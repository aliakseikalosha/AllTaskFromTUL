import cv2 as cv
import numpy as np

from scipy.ndimage import label
from cv04.cv04 import img_read
from cv07.cv07 import find_mass_center
from cv3.cv3 import show_img


def main():
    img = img_read("cv10_mince.jpg")
    gray = cv.cvtColor(img, cv.COLOR_RGB2GRAY)
    filterSize = (12, 12)
    kernel = cv.getStructuringElement(cv.MORPH_RECT, filterSize)

    gray = gray - cv.morphologyEx(gray, cv.MORPH_TOPHAT, kernel)
    gray = gray - cv.morphologyEx(gray, cv.MORPH_TOPHAT, kernel)
    show_img(gray, "gray")

    ret, thresh = cv.threshold(gray, 0, 255, cv.THRESH_BINARY_INV + cv.THRESH_OTSU)
    kernel = np.ones((3, 3))
    thresh = cv.erode(cv.dilate(thresh, kernel, iterations=5), kernel, iterations=5)
    show_img(thresh, "thresh")
    # noise removal
    kernel = np.ones((3, 3), np.uint8)
    opening = cv.morphologyEx(thresh, cv.MORPH_OPEN, kernel, iterations=2)
    # sure background area
    sure_bg = cv.dilate(opening, kernel, iterations=3)
    # Finding sure foreground area
    dist_transform = cv.distanceTransform(opening, cv.DIST_L2, 5)
    ret, sure_fg = cv.threshold(dist_transform, 0.7 * dist_transform.max(), 255, 0)
    # Finding unknown region
    sure_fg = np.uint8(sure_fg)
    unknown = cv.subtract(sure_bg, sure_fg)

    # Marker labelling
    ret, markers = cv.connectedComponents(sure_fg)
    # Add one to all labels so that sure background is not 0, but 1
    markers = markers + 1
    # Now, mark the region of unknown with zero
    markers[unknown == 255] = 0
    show_img(markers, "markers")
    markers = cv.watershed(img, markers)
    # img[markers == -1] = [255, 0, 0]
    markers[markers < 1] = 0
    show_img(markers)
    m = markers.max()
    c = find_mass_center(markers, range(2, m + 1), img)

    show_img(c, "center")


if __name__ == "__main__":
    main()
