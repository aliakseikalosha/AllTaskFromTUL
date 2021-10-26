import cv2
import matplotlib.pyplot as plt
import numpy as np
import struct


def imshow(data, show_color_bar=False):
    plt.imshow(data)
    if show_color_bar:
        plt.colorbar()
    plt.show()


def unpack4(f):
    return struct.unpack('i', f.read(4))[0]


def unpack2(f):
    return struct.unpack('h', f.read(2))[0]


def imread(path):
    with open(path, 'rb') as f:
        print(f.read(2))
        file_size_byte = unpack4(f)
        print(unpack4(f))
        byte_count_head = unpack4(f)
        print(unpack4(f))
        widht = unpack4(f)
        heigth = unpack4(f)
        plane_count = unpack2(f)
        bite_per_pixel = unpack2(f)
        compress_type = unpack4(f)
        size_byte = unpack4(f)
        pixel_per_metre_width = unpack4(f)
        pixel_per_metre_heigth = unpack4(f)
        color_count = unpack4(f)
        color_count_important = unpack4(f)
        width_add = 4 - int(widht * bite_per_pixel / 8) % 4
        data = np.zeros([widht, heigth, 3], dtype='B')

        def read_pixel():
            pixel = []
            n = 3
            if bite_per_pixel <= 8:
                n = 1
            for _ in range(0, n):
                pixel.append(struct.unpack('B', f.read(int(bite_per_pixel / n / 8)))[0])

            return pixel

        for y in range(0, heigth):
            for x in range(0, widht):
                data[x, y] = read_pixel()
            f.read(width_add)

        return data


def task1():
    bgr = cv2.imread('cv03_objekty1.bmp')
    rgb = cv2.cvtColor(bgr, cv2.COLOR_BGR2RGB)
    imshow(rgb)
    imshow(bgr)
    gray1 = cv2.cvtColor(rgb, cv2.COLOR_RGB2GRAY)
    plt.imshow(gray1, cmap='gray')
    plt.colorbar()
    plt.show()


def task2():
    img = imread("cv03_objekty1.bmp")
    img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    imshow(img)


def task3():
    img = cv2.imread("cv03_objekty1.bmp")
    img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

    new_img = cv2.cvtColor(img, cv2.COLOR_RGB2GRAY)
    imshow(new_img, True)

    new_img = cv2.cvtColor(img, cv2.COLOR_RGB2HSV)
    imshow(new_img[:, :, 0], True)
    imshow(new_img[:, :, 1], True)
    imshow(new_img[:, :, 2], True)

    new_img = cv2.cvtColor(img, cv2.COLOR_RGB2YCrCb)
    imshow(new_img[:, :, 0], True)
    imshow(new_img[:, :, 1], True)
    imshow(new_img[:, :, 2], True)


def task4():
    img = cv2.imread("cv03_red_object.jpg")
    img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    for x in range(0,img.shape[0]):
        for y in range(0, img.shape[1]):
            s = (float(img[x,y,0])+img[x,y,1]+img[x,y,2])
            if s < 1:
                img[x,y]=[255,255,255]
                continue

            r = img[x,y,0]/s
            if r < 0.5:
                img[x,y]=[255,255,255]
    imshow(img)


def main():
    task1()
    task2()
    task3()
    task4()


if __name__ == '__main__':
    main()
