import cv2
import matplotlib.pyplot as plt
import numpy as np


def img_show(left, right):
    f, (ax1, ax2) = plt.subplots(1, 2)
    ax1.imshow(left)
    ax2.imshow(right)
    plt.show()


def img_show_hist(img):
    f, (ax1, ax2) = plt.subplots(1, 2)
    ax1.imshow(img)
    ax2.plot(hist(img))
    plt.show()


def img_read(path):
    bgr = cv2.imread(path)
    rgb = cv2.cvtColor(bgr, cv2.COLOR_BGR2RGB)

    return rgb


def img_fix(path_img, path_fix):
    img = img_read(path_img)
    fix = img_read(path_fix)
    result = np.zeros(img.shape)

    for x in range(0, img.shape[0]):
        for y in range(0, img.shape[1]):
            for i in range(0, img.shape[2]):
                if fix[x, y, i] == 0:
                    result[x, y, i] = 1.0
                else:
                    result[x, y, i] = (img[x, y, i] / 255.0) / (fix[x, y, i] / 255.0)

    return img, result


def task01():
    img_show(*img_fix("Cv04_porucha1.bmp", "Cv04_porucha1_etalon.bmp"))
    img_show(*img_fix("Cv04_porucha2.bmp", "Cv04_porucha2_etalon.bmp"))


def hist(img):
    h = np.zeros(255)
    i = 0
    for x in range(0, img.shape[0]):
        for y in range(0, img.shape[1]):
            h[img[x, y]] += 1
            i += 1

    return h


def task02():
    img = img_read("Cv04_rentgen.bmp")
    gray = cv2.cvtColor(img, cv2.COLOR_RGB2GRAY)
    result = np.zeros(gray.shape).astype(np.uint8)
    h = hist(gray)

    for x in range(0, gray.shape[0]):
        for y in range(0, gray.shape[1]):
            c = gray[x, y]
            result[x, y] = 255 / (gray.shape[0] * gray.shape[1]) * (sum(h[0:c]))

    result_rgb = cv2.cvtColor(result, cv2.COLOR_GRAY2RGB)
    img_show(img, result_rgb)
    img_show_hist(img)
    img_show_hist(result_rgb)


def main():
    task01()
    task02()


if __name__ == "__main__":
    main()
