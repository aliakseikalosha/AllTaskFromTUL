from time import sleep

import cv2
import numpy as np
from matplotlib import pyplot as plt


def play_video():
    path = "cv08_video.mp4"
    cap = get_video(path)
    n_frames = int(cap.get(cv2.CAP_PROP_FRAME_COUNT))
    data = plot_diff_dct(path)
    red_lines = get_frame_data_for(data, 0.75)
    fig, ax = plt.subplots(1, 1)
    for i in range(1, n_frames):
        ret, bgr = cap.read()
        rgb = cv2.cvtColor(bgr, cv2.COLOR_BGR2RGB)
        ax.imshow(rgb, aspect='auto', extent=[0, len(data), min(red_lines), max(red_lines)])
        ax.plot(red_lines, 'r')
        ax.plot(data, 'b')
        ax.axvline(x=i, linewidth=2, color='g')
        plt.show(block=False)
        sleep(0.1)
        plt.close(fig)
        fig, ax = plt.subplots(1, 1)


def get_video(path):
    cap = cv2.VideoCapture(path)
    return cap


def frame_count(video):
    return int(video.get(cv2.CAP_PROP_FRAME_COUNT))


def plot_sum_diff(path):
    sums = []
    video = get_video(path)
    n_frames = frame_count(video)
    prev_frame = None
    for i in range(0, n_frames):
        ret, bgr = video.read()
        frame = cv2.cvtColor(bgr, cv2.COLOR_BGR2GRAY)
        if i == 0:
            prev_frame = frame
            continue
        current = frame
        sums.append(abs(sum(sum(prev_frame / 255)) - sum(sum(current / 255))))
        prev_frame = current

    return sums


def plot_diff_sum2(path):
    prev_frame = None
    diff = [0]
    sums = []
    video = get_video(path)
    n_frames = frame_count(video)
    for i in range(0, n_frames):
        ret, bgr = video.read()
        frame = cv2.cvtColor(bgr, cv2.COLOR_BGR2RGB)
        if i == 0:
            prev_frame = cv2.cvtColor(frame, cv2.COLOR_RGB2GRAY)
            continue
        current = cv2.cvtColor(frame, cv2.COLOR_RGB2GRAY)
        diff.append(abs(sum(sum(prev_frame / 255 - current / 255))))
        sums.append(abs(diff[i - 1] - diff[i]))
        prev_frame = current

    return sums


def plot_diff_sum(path):
    prev_frame = None
    diff = []
    video = get_video(path)
    n_frames = frame_count(video)
    for i in range(0, n_frames):
        ret, bgr = video.read()
        frame = cv2.cvtColor(bgr, cv2.COLOR_BGR2RGB)
        if i == 0:
            prev_frame = cv2.cvtColor(frame, cv2.COLOR_RGB2GRAY)
            continue
        current = cv2.cvtColor(frame, cv2.COLOR_RGB2GRAY)
        diff.append(sum(sum(abs(prev_frame / 255 - current / 255))))
        prev_frame = current

    return diff


def plot_diff_hist(path):
    prev_frame = None
    diff = []
    video = get_video(path)
    n_frames = frame_count(video)
    for i in range(0, n_frames):
        ret, bgr = video.read()
        frame = cv2.calcHist(cv2.cvtColor(bgr, cv2.COLOR_BGR2GRAY), [0], None, [255], [0, 255])
        if i == 0:
            prev_frame = frame
            continue
        current = frame
        diff.append(sum(abs(prev_frame - current)))
        prev_frame = current

    return diff


def plot_diff_hist_np(path):
    prev_frame = None
    diff = []
    video = get_video(path)
    n_frames = frame_count(video)
    for i in range(0, n_frames):
        ret, bgr = video.read()
        frame, edge = np.histogram(cv2.cvtColor(bgr, cv2.COLOR_BGR2GRAY), bins=255, range=(0, 255))
        if i == 0:
            prev_frame = frame
            continue
        current = frame
        diff.append(np.sum(np.abs(prev_frame - current)))
        prev_frame = current

    return diff


def get_dct_of_image(image):
    imf = np.float32(image) / 255.0
    return cv2.dct(imf)


def plot_diff_dct(path):
    prev_frame = None
    diff = []
    video = get_video(path)
    n_frames = frame_count(video)
    for i in range(0, n_frames):
        ret, bgr = video.read()
        frame = get_dct_of_image(cv2.cvtColor(bgr, cv2.COLOR_BGR2GRAY))
        if i == 0:
            prev_frame = frame
            continue
        current = frame
        diff.append((abs(sum(sum(prev_frame - current)))))
        prev_frame = current

    return diff


def get_frame_data_for(data, threshold_percent):
    threshold = max(data) * threshold_percent
    red_lines = []
    for d in data:
        red_lines.append(d if d < threshold else max(data) * 1.1)
    return red_lines


def plot(ax, data, title, threshold_percent=0.75):
    ax.plot(get_frame_data_for(data, threshold_percent), 'r')
    ax.plot(data, 'b')
    ax.set_title(title)


def main():
    fig, axs = plt.subplots(2, 2)
    plot(axs[0, 0], plot_sum_diff("cv08_video.mp4"), "A", 0.25)
    plot(axs[0, 1], plot_diff_sum("cv08_video.mp4"), "B")
    plot(axs[1, 0], plot_diff_hist_np("cv08_video.mp4"), "C")
    plot(axs[1, 1], plot_diff_dct("cv08_video.mp4"), "D")
    plt.show(block=False)
    sleep(1)
    play_video()


if __name__ == "__main__":
    main()
