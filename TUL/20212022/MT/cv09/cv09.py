import cv2
import numpy as np
from matplotlib import pyplot as plt


def get_pca_components(img):
    vectors = img_to_vectors(img)

    mean_v = np.zeros(vectors.shape[1])
    for i in range(0, vectors.shape[0]):
        mean_v = (mean_v + vectors[i])

    mean_v /= vectors.shape[0]

    w = np.zeros(vectors.shape)
    for i in range(0, vectors.shape[0]):
        w[i] = vectors[i] - mean_v

    w_t = np.transpose(w)
    c = w @ w_t

    eig_n, eig_v = np.linalg.eig(c)
    sort_index = eig_n.argsort()
    eig_v_sorted = eig_v[sort_index[::-1]]
    e = eig_v_sorted @ w

    k = np.zeros(e.shape)
    for i in range(0, vectors.shape[0]):
        k[i] = e[i] + mean_v

    return k


def img_to_vectors(img):
    shape = img.shape
    vectors = np.zeros([shape[2], shape[0] * shape[1]])
    for x in range(0, shape[0]):
        for y in range(0, shape[1]):
            for z in range(0, shape[2]):
                vectors[z][x + y * shape[0]] = img[x, y, z]

    return vectors


def vectors_to_img(vectors, shape):
    img = np.zeros(shape)
    for x in range(0, shape[0]):
        for y in range(0, shape[1]):
            if shape[2] == 1:
                img[x, y, 0] = vectors[x + y * shape[0]]
            else:
                for z in range(0, shape[2]):
                    img[x, y, z] = vectors[z][x + y * shape[0]]

    return img


def main():
    img = cv2.cvtColor(cv2.imread("Cv09_obr.bmp"), cv2.COLOR_BGR2RGB)
    k = get_pca_components(img)
    k1_img = vectors_to_img(k[0], [img.shape[0], img.shape[1], 1])
    k2_img = vectors_to_img(k[1], [img.shape[0], img.shape[1], 1])
    k3_img = vectors_to_img(k[2], [img.shape[0], img.shape[1], 1])
    fix, ax = plt.subplots(2, 2)

    ax[0,0].imshow(cv2.cvtColor(img, cv2.COLOR_RGB2GRAY), cmap='gray', vmin=0, vmax=255)
    ax[0,0].set_title("gray")

    ax[0,1].imshow(k1_img, cmap='gray')
    ax[0,1].set_title("k1")

    ax[1,0].imshow(k2_img, cmap='gray')
    ax[1,0].set_title("k2")

    ax[1,1].imshow(k3_img, cmap='gray')
    ax[1,1].set_title("k3")

    plt.show()


if __name__ == "__main__":
    main()
