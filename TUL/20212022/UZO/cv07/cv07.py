import cv2
import numpy as np

from cv04.cv04 import img_read
from cv06.cv06 import pixel_processor
from cv3.cv3 import show_img


def segment_img(img, is_over_the_edge):
    def process(x, y, shape):
        if is_over_the_edge(img[x, y]):
            return [0, 0, 0]
        return [1, 1, 1]

    processed = pixel_processor(img, process)
    return cv2.cvtColor(np.float32(processed), cv2.COLOR_RGB2GRAY)


def find_shapes(img):
    counter = 2
    directions = [(-1, -1), (0, -1), (1, -1), (-1, 0), (0, 0)]
    processed = np.zeros(img.shape)
    in_shape = False
    same = {}

    def have_key(k):
        if k in same:
            return True
        else:
            return False

    def get_keys_for(val):
        keys = []
        for k in same.keys():
            for value in same[k]:
                if value == val:
                    keys.append(k)
        if len(keys) > 0:
            return keys
        return None

    def add(key, s):
        if have_key(key):
            for val in s:
                if val not in same[key] and val != key:
                    same[key].append(val)
        else:
            k = get_keys_for(key)
            if k is None:
                same[key] = s
            else:
                add(min(k), s)

    def reduce_same():
        keys = same.keys()
        key_del = []
        for k in keys:
            for val in same[k]:
                if have_key(val) and k != val:
                    same[k].extend(same[val])
                    same[k] = list(set(same[k]))
                    if val not in key_del:
                        key_del.append(val)
                pos_keys = get_keys_for(val)
                if pos_keys is not None:
                    for pos_key in pos_keys:
                        min_pos_key = min(pos_keys)
                        if min_pos_key != pos_key:
                            same[min_pos_key].extend(same[pos_key])
                            same[min_pos_key].append(pos_key)
                            same[min_pos_key] = list(set(same[min_pos_key]))
                            if pos_key not in key_del:
                                key_del.append(pos_key)

        for k in key_del:
            del same[k]

        count = 1
        nice_same = {}
        for k in same.keys():
            nice_same[count] = same[k]
            count += 1
        return nice_same

    for x in range(img.shape[0]):
        for y in range(img.shape[1]):
            v = 0
            if img[x, y] < 1:
                if in_shape:
                    counter += 1
                    in_shape = False
            else:
                in_shape = True
                for d in directions:
                    try:
                        t = processed[x + d[0], y + d[1]]
                        s = [counter]
                        if t > 1 and t != counter:
                            s.append(t)

                        key = min(s)
                        add(key, s)
                        v = key
                    except IndexError:
                        pass

            processed[x, y] = v
    same = reduce_same()
    zones = []
    for x in range(img.shape[0]):
        for y in range(img.shape[1]):
            v = processed[x, y]
            if v > 0:
                zones.append(min(get_keys_for(v)))
                processed[x, y] = min(get_keys_for(v))
    return processed, set(zones)


def find_mass_center(img, segments, target_img=None, find_korun=False):
    if target_img is None:
        target_img = img

    processed = np.copy(target_img)
    color = max(segments) + 1

    if len(processed.shape) > 2 and processed.shape[2] > 1:
        color = [255.0, 0.0, 0.0]

    money_total = 0
    for i in segments:
        pixels = np.where(img == i)
        x = np.sum(np.power(pixels[0], 0) * np.power(pixels[1], 1)) / np.sum(
            np.power(pixels[0], 0) * np.power(pixels[1], 0))
        y = np.sum(np.power(pixels[0], 1) * np.power(pixels[1], 0)) / np.sum(
            np.power(pixels[0], 0) * np.power(pixels[1], 0))
        cost = 5 if len(pixels[0]) > 4000 else 1
        if find_korun:
            print(f'[{x},{y}] je {5 if len(pixels[0]) > 4000 else 1} koruna')
        money_total += cost
        processed = cv2.circle(np.float32(processed), (int(x), int(y)), radius=0, color=color, thickness=3)
    if find_korun:
        print(f'celkove je to {money_total} korun')
    return processed/255


def main():
    def is_segment(p):
        g = (255 * p[1]) / np.sum(p)
        if g >= 100:
            return True
        return False

    img_coins = img_read("cv07_segmentace.bmp")
    img_test = img_read("cv07_barveni.bmp", cv2.COLOR_RGB2GRAY) / 255
    mask_coins = segment_img(img_coins, is_segment)
    segment_coin, segments = find_shapes(mask_coins)
    show_img(mask_coins, "Segmentační maska", "gray")
    show_img(segment_coin, "Nalezeny objekty")
    show_img(find_mass_center(segment_coin, segments), "Nalezeny objekty s centrem")


if __name__ == "__main__":
    main()
