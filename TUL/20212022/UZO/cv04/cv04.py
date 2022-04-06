import cv2
import numpy as np

from cv1.cv1 import calc_hist
from cv3.cv3 import show_img


def img_read(path, color=None):
    if color is None:
        color = cv2.COLOR_BGR2RGB

    img = cv2.cvtColor(cv2.imread(path), color)
    return img


def distances(target, images, channel=0):
    dist = {x: 0 for x in range(len(images))}
    h = calc_hist(target, channel)
    for i in dist.keys():
        dist[i] = np.sum(np.abs(h - calc_hist(images[i], channel)))
    sort_keys = [k for k, v in sorted(dist.items(), key=lambda item: item[1])]
    return sort_keys


def task03(count):
    images = [img_read("../cv3/img/im0" + str(i + 1) + ".jpg") for i in range(0, count)]
    images_gray = [cv2.cvtColor(img, cv2.COLOR_RGB2GRAY) for img in images]
    size = (300, 300, 3)
    offset = (50, 50, 3)
    table = np.ones([int(images[0].shape[0] * (count + 2)),
                     images[0].shape[1] * (count + 2),
                     images[0].shape[2]]) * 128
    for i in range(0, count):
        dist = distances(images_gray[i], images_gray)
        for j in range(len(dist)):
            x = i * size[0] + offset[0]
            y = (j) * size[1] + offset[1]
            img = images[dist[j]]
            try:
                table[x:x + img.shape[0], y:y + img.shape[1], :] = img
            except Exception:
                print(j, "|", y, ":", y + images[j].shape[1], "->", y + images[j].shape[1] - y, "==", images[j].shape)
    show_img(table / 256)


def main():
    task03(9)


if __name__ == "__main__":
    main()
