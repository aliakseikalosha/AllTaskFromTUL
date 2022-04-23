import cv2
import numpy as np

from cv04.cv04 import img_read
from cv07.cv07 import segment_img, find_shapes, find_mass_center
from cv3.cv3 import show_img


def process_img(path, segmentation, seg_clr=None):
    img = img_read(path)
    show_img(img, f"Puvodní obrázek {path}")
    seg_img = segment_img(img if seg_clr is None else cv2.cvtColor(img, seg_clr), segmentation)
    show_img(seg_img, f"Binarní obrázek {path}", cmap="gray")
    kernel = np.ones((5, 5), np.uint8)
    clean_img = cv2.dilate(cv2.erode(seg_img, kernel), kernel)
    show_img(clean_img, f"Vyčištěný obrázek {path}", cmap="gray")
    shape_img, zones = find_shapes(clean_img)
    show_img(shape_img, f"Obrázek {path} rozdelený na segmenty")
    show_img(find_mass_center(shape_img, zones, img), f"{path} s vkreslenými centry teček")


def main():
    process_img('cv08_im1.bmp', lambda p: max(p) > 96)
    process_img('cv08_im2.bmp', lambda p: 100 < p[0] < 120, cv2.COLOR_RGB2HSV)


if __name__ == "__main__":
    main()
