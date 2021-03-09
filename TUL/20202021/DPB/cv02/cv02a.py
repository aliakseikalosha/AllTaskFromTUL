# koment
# výpis na výstup
print('Hello World')  # lze použít i dvojité uvozovky "Hello World"

# podmínka if else

if 1 > 0:
	print('Ok')
else:
	pass  # neudělá níc - používá se jako placeholder pro budoucí kód

# proměnné
a = 1
b = "test"
c = True  # nebo False

# vypsání typu
print(type(a))

# pole - vytvoření, indexování a procházení
arr = ['Hello', 'World']
arr2 = [0, 1, 2, 3, 4]

print(arr2[-1])  # poslední prvek
print(arr2[::-1])  # výpis od konce
print(arr2[1:3])  # výpis části pole

# nebo s využitím range built-in funkce
arr3 = [x for x in range(5)]  # [0, 1, 2, 3, 4]

print(arr3)

print(arr[0])  # indexování od 0

for item in arr:
	print(item)

# pokud je potřeba i index
for i, item in enumerate(arr):
	print('Na indexu {} je {}'.format(i, item))

# tuple - seřazenná a neměnná struktura
t = (1, 2, 3)
print(t[0])
# nelze t[0] = 0

# sety - neseřazená a neindexovaná množina
s = {'a', 'b', 'c'}
s2 = set(['a', 'a', 'b', 'c'])
print(s2)

# slovník (dict)
a = {}
b = {
	'a': 1,
	'b': 'test',
}
print(b['a'])
b['c'] = 0

print(b)

for key, val in b.items():
	print('{}: {}'.format(key, val))


# funkce
def add(a, b):
	return a + b


print(add(1, 2))


def test(a, b):
	return a + 1, b + 1


a, b = test(1, 2)
a, _ = test(1, 2)  # funkce vrací více parametrů, ale nás zajímá jen jeden - druhý se zahodí


# objekt

class Person:
	def __init__(self, name, age):
		self.name = name
		self.age = age

	def hello(self):
		print("Hello my name is " + self.name)


p = Person('Jarda', 20)
print(p.name)
p.hello()

# práce se soubory
with open('test.txt', 'w', encoding='utf8') as f:
	f.write('Hello\nWorld')

with open('test.txt', 'r', encoding='utf-8') as f:
	lines = [line.strip() for line in f.readlines()]  # strip odstraní \n
	print(' '.join(lines))  # join spojí řetězce v poli s uvedeným řetězcem mezi nimi
