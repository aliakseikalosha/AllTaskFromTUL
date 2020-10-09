#define _CRT_SECURE_NO_WARNINGS   
#include <stdio.h>

int main() {
	//1. zápis do souboru
	// zadání: nauète se zapsat 3 hodnoty typu int do souboru
	// deklarujeme promìnnou typu FILE. Jedná se pointer (poznáte díky * pøed názvem promìnné). Zatím prosím berte jako fakt, pointerùm se budeme vìnovat hodnì,
	// a studenti je vìtšinou z poèátku moc rádi nemají.

	FILE *fw;
	int a, b, c;
	// naètení hodnot do promìnných a, b, c
	printf("Zadej tri int hodnoty oddelene mezerou a stiskni enter \n");
	scanf("%d %d %d", &a, &b, &c);
	// otevøeme soubor (bude otevøen v aktuálním pracovním adresáøi) v režimu zápis ("w")
	fw = fopen("SOUBOR.TXT", "w");
	// zapíšeme do souboru naètené hodnoty
	fprintf(fw, "%d %d %d \n", a, b, c);
	//zavøeme soubor, je velmi dùležité po sobì uklidit. V C a C++ to mùže mít fatální následky
	fclose(fw);


	//2. pøípis do souboru
	//jediná zásadní zmìna oproti výše uvedenému zápisu do souboru, je režim otevøení souboru, tentokráte "a"
	FILE *fw2;
	int aa, bb, cc;
	printf("Zadej tri int hodnoty oddelene mezerou a stiskni enter \n");
	scanf("%d %d %d", &aa, &bb, &cc);
	fw2 = fopen("SOUBOR.TXT", "a");
	fprintf(fw2, "%d %d %d \n", aa, bb, cc);
	fclose(fw2);

	//3. nacteni ze souboru
	FILE *fr2;
	fr2 = fopen("SOUBOR.TXT", "r");
	//využíváme toho, že funkce fscanf vrací EOF, když dojte na konec souboru
	while (fscanf(fr2, "%d %d %d", &a, &b, &c) != EOF)
		printf("%d %d %d \n", a, b, c);
	fclose(fr2);



	//4. cteni cele radky
	 /*
		int c;
		FILE *fr;

		fr = fopen("SOUBOR.TXT", "r");
		while ((c = getc(fr)) != '\n')
			putchar(c);

		putchar('\n');
		fclose(fr);
	*/

	//5. kopirovani souboru
	FILE *fr3, *fw3;
	int ccc;
	/* musí byt deklarována jak int a ne char, protože  EOF == -1!!!, tedy je definované jako èíslo a ne znak */
	fr3 = fopen("S1.TXT", "r"); //soubor musí existovat, jinak bude hlásit chybu. 
	//? Umìli byste toto ošetøit a místo chyby napsat uživateli, že soubor neexistuje?
	fw3 = fopen("S2.txt", "w");

	while ((ccc = getc(fr3)) != EOF)
		putc(ccc, fw3);

	fclose(fr3);
	fclose(fw3);


	//6.Co na vstupu to na vystupu 	
	FILE *fw4;
	int cccc;
	fw4 = stdout;
	printf("Napiste text, stisknete '*' pro konec.\n");

	while ((cccc = getchar()) != '*')
		putc(cccc, fw4);

	return 0;

	//7. binarni soubory
	FILE *f;
	int i = 5;
	double d = 3.13345;
	// otevíráme v režimu binárního ètení (již nebudeme èíst po velikosti char jako v pøedchozím pøípadì, ale po velikosti jakou zadáme
	f = fopen("DATA.DAT", "wb+");
	//èteme po tolika bytech, kolik je velikost i, což je deklarované jako int a tedy èteme jeden int. Makro sizeof vrací poèet byte promìnné nebo typu zadaného jako parametr
	fwrite(&i, sizeof(i), 1, f);
	fwrite(&d, sizeof(d), 1, f);

	printf("Pozice v souboru je %ld \n", ftell(f));
	fseek(f, 0, SEEK_SET); // posun na zaèátek souboru
	i = 0; d = 0.0;
	fread(&i, sizeof(i), 1, f); //zkusit chybu sizeof(d)
	fread(&d, sizeof(d), 1, f);
	printf("Nactena data: i = %d, d = %f \n", i, d);

	fclose(f);
	return 0;

}

