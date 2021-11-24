import cv2
import matplotlib
import numpy as np
from matplotlib import pyplot as plt
from matplotlib.pyplot import pause


def play_video():
    cap = get_video("cv08_video.mp4")
    n_frames = int(cap.get(cv2.CAP_PROP_FRAME_COUNT))

    fig1, ax1 = plt.subplots()
    for i in range(1, n_frames):
        ret, bgr = cap.read()
        rgb = cv2.cvtColor(bgr, cv2.COLOR_BGR2RGB)
        ax1.imshow(rgb)
        plt.show()
        pause(0.001)


def get_video(path):
    cap = cv2.VideoCapture(path)
    return cap


def frame_count(video):
    return int(video.get(cv2.CAP_PROP_FRAME_COUNT))


def plot_sum_diff(path):
    prev_frame = None
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
        sums.append(abs(sum(sum(prev_frame/255)) - sum(sum(current/255))))
        prev_frame = current

    plt.plot(sums)
    plt.show()


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
        sums.append(abs(diff[i-1] - diff[i]))
        prev_frame = current

    plt.plot(sums)
    plt.show()


def main():
    plot_sum_diff("cv08_video.mp4")
    plot_diff_sum2("cv08_video.mp4")
    pass


if __name__ == "__main__":
    main()
