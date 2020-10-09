//1. definujeme novou konstantu kvùli error unsafe pro scanf, zatím prosím berte jako fakt, vysvìtlím pozdìji
#define _CRT_SECURE_NO_WARNINGS   

//2. vložíme knihovny, které obsahují námi využívané funkce
// pokud bychom to neudìlali, tak by nám pøekladaè dával chybu napø.:
// scanf : identifikátor se nenašel
#include <stdio.h>

//3. pøipravíme základní metodu main, která se spustí po úspìšném pøekladu zdrojového kódu
int main() {
	//4. zaèátek bloku 
	// funkce getchar a putchar, pro základní ètení a zápis z a na výstupní stream (konzoli)

	int c;
	c = getchar();
	putchar(c);

	//4. konec bloku
	printf("\n------------------------\n5. funkce pro formatovane cteni a zapis \n------------------------\n");
	//5. zaèátek bloku
	// funkce pro formátované ètení a zápis

	// 5.1 printf nám umožní zapisovat na výstupní stream. Má dva parametry, první øetìzec nám øíká jak bude vypadat formát výstupu
	// (v tomto pøípadì, že se bude vypisovat øetìzec)
	// a druhý parametr je promìnná/konstanta, která se má vypsat 
	// øetìzec \n je takzvaný formátovací a tento bude jeden z nejpoužívanìjších a øíká, že se má odøádkovat
	printf("%s", "Ahoj\n");
	//? Umìli byste použít printf aby vypsal:
	// Ahoj
	// sve
	// te

	// definice promìnných i a j, zatím bez hodnoty.
	// je zásadní si pamatovat, že po deklarace promìnné v C a C++ tato nemá implicitní hodnotu, tedy napø/ 0.
	int i, j;
	// zkuste napøíklad:
	//	printf("i = %d - j = %d", i, j);
	// v lepším pøípad dostanete od pøekladaèe "vynadáno", že používáte neinicializovanou promìnnou. 
	// na to prosím dávejte v C a C++ veliký pozor, C ani C++ neudìlá nic, co mu neøeknete, aby udìlalo (inicializace promìnných, kontrola mezí polí atd.).
	// díky tomu je, ale v porovnání s napø. Java èi C#, extrémnì rychlý a proto se stále využívá a je vyžadovaný

	//5.2 formátované ètení do promìnné pomocí scanf
	printf("zadej dve cisla int \n");
	scanf("%d", &i);
	scanf("%d", &j);
	//5.3 formátovaný výpis promìnné pomocí printf
	printf("%d %d \n", j, i);
	printf("i + j = %d \n", i + j);

	//5. konec bloku

	//6. vyhodnocovaní výrazu
	printf("\n------------------------\n6. vyhodnocovani vyrazu \n------------------------\n");
	// vždy je vyhodnocuje zleva doprava, lze toho využít pøi napø. kontrole dìlení nulou viz níže.
	int y = 3;
	int x = 10;
	int z = 10; //pozor na inicializaci
// díky vyhodnocení zleva doprava nikdy nedojet k dìlení nulou
	if (y != 0 && x / y < z) {
		//pozor takto nebude vypisovat desetinné èíslo, protože prostì vezme obsah adresy, kam se uloží x / y a oèekává, že to je int a vypíše tedy jen tu èást
		// co má odpovídající poèet bytu
		printf("%d \n", x / y);

		//i s desetinou èástí
		// je nutn0 i pøetypování y, aby pøekladaè pochopil, že výsledek bude desetinné èíslo
		printf("%.4f \n", x / (float)y);
	}
	else {
		// v C nejsou výjimky, vše se dìlá pomocí návratových hodnot funkcí èi pomocí if a else jako v tomto pøípadì
		// výjimky najdeme až v C++
		printf("deleni nulou\n");
	}

	getchar();
	getchar();

}
