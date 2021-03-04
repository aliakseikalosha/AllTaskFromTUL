def Fibonacci(n):
    numbers = list(range(n))
    if n == 0:
        return None
    elif n == 1:
        return 1
    elif n == 2:
        return [1, 1]
    else:
        numbers[0] = 1
        numbers[1] = 1
        for i in range(2, n):
            numbers[i] = numbers[i - 1] + numbers[i - 2]
    return numbers


'''
print(Fibonacci(0))
print(Fibonacci(1))
print(Fibonacci(2))
print(Fibonacci(3))
print(Fibonacci(10))
'''


def pyramid(n):
    center = int(((n - 1) * 2 + 1) / 2)
    for i in range(n):
        for j in range(n * 2 + 1):
            if j < center - i or j > center + i:
                print(' ', end=' ')
            else:
                print('#', end=' ')
        print('')


'''
pyramid(5)
pyramid(9)
'''


def is_prime(n):
    for i in range(2, n - 1):
        _, a = divmod(n, i)
        if a == 0:
            return False
    return True


'''
print(is_prime(3))
print(is_prime(11))
print(is_prime(10))
'''


def pow(c, n):
    return c ** n


'''
print(pow(1,2))
print(pow(2,10))
'''


def dig_sum(n):
    sum = 0
    for i in str(n):
        sum += int(i)
    return sum


'''
print(dig_sum(1))
print(dig_sum(10))
print(dig_sum(123))
print(dig_sum(1234))
'''


def is_palindrom(str):
    return str == str[::-1]


'''
print(is_palindrom("abba"))
print(is_palindrom("alba"))
print(is_palindrom("ahoj"))
'''
