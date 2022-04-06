import cv2
import numpy as np

from cv04.cv04 import img_read
from cv06.cv06 import pixel_processor
from cv3.cv3 import show_img


def segment_img(img):
    def process(x, y, shape):
        p = img[x, y]
        g = (255 * p[1]) / np.sum(p)
        if g >= 100:
            return [0, 0, 0]
        return [1, 1, 1]

    processed = pixel_processor(img, process)
    return cv2.cvtColor(np.float32(processed), cv2.COLOR_RGB2GRAY)


def find_shapes(img):
    counter = 2
    directions = [(-1, -1), (0, -1), (1, -1), (-1, 0), (0, 0)]
    processed = np.zeros(img.shape)

    for x in range(img.shape[0]):
        for y in range(img.shape[1]):
            if img[x, y] < 1:
                counter += 1
                v = 0
            else:
                v = img[x, y]
                for d in directions:
                    t = img[x + d[0], y + d[1]]
                    if t > 0 and t != v:
                        v = counter
            print(x, y, "\n")
            processed[x, y] = v
    return processed


def main():
    img_coins = img_read("cv07_segmentace.bmp")
    img_test = img_read("cv07_barveni.bmp", cv2.COLOR_RGB2GRAY)
    # mask_coins = segment_img(img_coins)
    # show_img(mask_coins, "Segmentační maska", "gray")
    show_img(find_shapes(img_test), "Nalezeny objecty")


if __name__ == "__main__":
    main()
