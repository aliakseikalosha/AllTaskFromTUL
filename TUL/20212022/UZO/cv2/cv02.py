import cv2 as cv


def find_on_image(img, target):
    (height, width, _) = target.shape
    i = cv.cvtColor(img, cv.COLOR_RGB2HSV)
    t = cv.cvtColor(target, cv.COLOR_RGB2HSV)

    return 0, 0, width, height
