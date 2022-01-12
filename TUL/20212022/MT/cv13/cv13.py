def get_control_for(n):
    if need_to_invert_control(n):
        return ~n & 0xFF
    return n


def need_to_invert_control(n):
    if bin(n)[2:].count("0") % 2:
        return True
    return False


def check(n, c):
    k = (255 - c) if (need_to_invert_control(n)) else c
    if need_to_invert_control(n):
        return (255 - (n ^ k)) & n
    else:
        if k != c:
            return (n ^ k) & n
        return (n ^ k) ^ n


def main():
    numbers = [(160, 223), (64, 65), (128, 126)]  # 32, 65, 128
    for n, c in numbers:
        print(f'({n},{c}) -> {check(n, c)}')
    pass


if __name__ == "__main__":
    main()
