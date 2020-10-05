clear all;
clc;
close all;
echo on;

%                      MATLAB - Jednoduchy uvod


% MATLAB je interaktivni program pro praci s maticemi, 
% ktery umoznuje numericke a graficke reseni vedeckych 
% a technickych problemu.

% Takto vypada prikazovy promt >>. 
% Seznam vsech dostupnych prikazu se objevi po napsani  "help".
% Strucny popis zvolene funkce dostaneme napsanim prikazu help, za
% nimz nasleduje jmeno zvolene funkce.

pause  %stiskni klavesu

% Priklad: chceme popis funkce tan (tangens), napiseme tedy
help tan


pause  %stiskni klavesu


%OPUSTENI MATLABu
% Napis bud "exit" nebo "quit".

%DULEZITA POZNAMKA
%MATLAB rozlisuje mala a velka pismena! 

%Prikazy "casesen off" nebo "casesen on" lze rozlisovani (sensitivitu) nastavit.

%ZAKLADY
% Scitani, odecitani, nasobeni, deleni  se zadava beznyni operatory "+, -, *, /".
% Take rada beznych funkci ma obvykla jmena.   

pause  %stiskni klavesu
clc

%Priklad: prikaz MATLABu 
%sin(0.25*pi)

% Zde se vsimneme, ze 
% a) "pi" je vestavena konstanta
% b) "sin" je vestavena funkce
% c) argument u funkci typu sinus apod je zasadne udavan v radianech
% d) cisla mohou byt zadavana mnoha zpusoby, napr.  0.25, 1/4 or 2.5e-1.

% Po stisku ENTER je prikaz okamzite vykonan a objevi se odpoved ve tvaru
% ans
%  = 0.7071

sin(0.25*pi)

pause  %stiskni klavesu

% Promennou ans (od slova answer) pouzije system, vzdy kdyz neni 
% specifikovana zadna promenna, do niz by mel byt vysledek zapsan.

% Kdyz promennou specifikujeme, je vysledek teto promenne prirazen napr. 
s=sin(pi/4)

pause  %stiskni klavesu

% Promenna si pamatuje prirazenou hodnotu tak dlouho,
% dokud neni tato hodnota zmenena jinym prirazenim nebo vymazana
% prikazem "clear" napr. clear s.

pause  %stiskni klavesu

clc 
% Dalsi prikazy a poznamky:
% "clc" maze obrazovku
% "pause" ceka na stisk libovolne klavesy
% znak "%" procento uvozuje poznamku

pause  %stiskni klavesu

% FORMAT CISEL
% MATLAB zobrazuje vysledky v kratkem formatu, i kdyz v pameti
% jsou vysledky a promenne reprezentovany v dvojite presnosti.

% Chceme-li prepnout na zobrazovani v dvojite presnosti, napiseme
format long e

% Kdyz nyni zadame jmeno promenne "s", objevi se v dvoj. presnosti
s

pause  %stiskni klavesu

% Navrat k standardnimu (kratkemu) formatu se uskutecni napsanim
format
s

pause  %stiskni klavesu

clc

% RADKOVE VEKTORY (ROW VECTORS, ARRAYS)

% Radkovy vektor snadno vygenerujeme zapsanim jednotlivych prvku (cisel) 
% oddelenych carkou ci mezerou a uzavreni do hranatych zavorek. Prikaz

x=[1 3 5 7]      % ulozi pole hodnot 1 3 5 7 do promenne x

% TRI TECKY (ELLIPSIS)
% Prikaz,  ktery se nevejde na radek, lze zapsat na dva radky, pricemz 
% na prvnim radku napiseme tri tecky a stiskneme ENTER. 
% Priklad:
% x=[1  2  3  4 ...
% 5  6]

x=[1  2  3  4 ...
5  6]

pause  %stiskni klavesu
clc

% DVOJTECKA (COLON)
% Vytvoreni dlouhych poli lze usnadnit pouzitim dvojtecky ":". Napr.
x=1:6

% vytvori radkovy vektor [1  2  3  4  5  6]

% Standarni prirustek je 1. Pokud chceme jiny, napiseme 

y=0.2:.2:1.1  % coz znamena od 0.2 do 1.1 s prirustkem 0.2

pause  %stiskni klavesu
clc

% STREDNIK (SEMI-COLON)
% Strednik ";" pouzity za prikazem zpusobi, ze se potlaci vypis hodnot. 
% Priklad: porovnejte rozdil mezi prikazy
% z=1:300   a  z=1:300;

z=1:300

pause  %stiskni klavesu

z=1:300;

pause  %stiskni klavesu

% Samozrejme strednik pouze potlacil vypis, ale vektor z opravdu existuje.
% Lze se presvedcit, vypsanim napr. prvniho prvku

z(1)   % vypise prvni prvek pole

pause  %stiskni klavesu
clc

% ZRUSENI VYPISU NA OBRAZOVKU CI PROVADENI PROGRAMU

% Chcete-li skoncit dlouho trvajici vypis na obrazovku ci
% zastavit prave provadeny prikaz, stisknete "CTRL-C".

pause  %stiskni klavesu
clc

% CARKA (COMMA), STREDNIK A NEKOLIK PRIKAZU NA RADKU
% Na jeden radek lze umistit vice prikazu. Musi byt oddeleny
% carkou ci strednikem. Strednik potlaci vypis, carka ne, viz priklad.

x=2,y=3*x*x;z=y-6*x  % vypise x a z ale ne y

pause  %stiskni klavesu
clc

% PROMENNE A MATICE 

% Kazda promenna v MATLABu je povazovana za matici!!!. I kdyz ma rozmer
% napr. 1x1 (jednoduche cislo). 
% Dimenzovani ani deklarovani typu se nepouziva, MATLAB automaticky 
% alokuje prislusny prostor. Index u matic zacina od 1 (ne od 0 jako napr. 
% u Cecka). Jednotlive prvky se adresuji radkovym a sloupcovym indexem
% napr. C(2,3) je prvek v druhem radku a tretim sloupci.

pause  %stiskni klavesu
clc

% Kdyz napiseme

A(2,2)=3

% i kdyz predtim matice A nebyla definovana, MATLAB okamzite vytvori matici A o velikosti 2x2 a prvku A(2,2) priradi hodnotu.

pause  %stiskni klavesu

% Jestlize pak napiseme

A(2,3)=4

% pamet alokovana pro matici A se automaticky rozsiri. 

pause  %stiskni klavesu

% Zmenit hodnotu prvku matice lze snadno, napr.
A(1,2)=5

% Prvky matice lze zadavat jako vektory, pricemz jednotlive radky 
% jsou oddeleny strednikem, napr,

b=[1 2 4; 3 1 2]   % vytvori matici 3x2 matrix

% DULEZITE: V tomto formatu vsechny radky musi mit stejny pocet prvku.

pause  %stiskni klavesu
clc

% ZNOVU O DVOJTECCE
% Dvojtecka je take pouzivana pro oznaceni celeho rozsahu hodnot.
% Napr.  A(2,3) oznacuje jeden prvek natice A, zatimco
% A(2,:) cely druhy radek A.
% Cili napr. 

c=b(2,:)  % priradi cely 2. radek matice b promenne (vektoru) c

% podobne 

b(:,3)     % oznacuje cely 3.sloupec b

pause  %stiskni klavesu
clc

% Podobne prikaz x=y(:) vezme prvky y sloupec po sloupci a umisti je 
% (zretezi) do jedineho sloupcoveho vektoru.  
% Toho lze vyuzit i tak, ze lze na prvky matice odkazovat jedinym indexem, 
% ktery ma vyznam indexu u vektoru vznikleho zretezenim sloupcu. Priklad:

a=[1 2 3;4 5 6;7 8 9]  % a je matice 3x3

% u niz plati a(1)=1, a(2)=4, a(3)=7, a(4)=2 etc. Muzeme to vyzkouset

b=a(4)  % melo by byt rovno 2

pause  %stiskni klavesu

% Abychom videli, jak vypada zretezena matice, napisme

a(:)   

pause  %stiskni klavesu
clc

% NEKTERE DULEZITE MATICE v MATLABu

a=zeros(2,3)  % matice 2x3 s vsemi prvky rovnymi nule
b=ones(2,3)   % matice 2x3 s vsemi prvky rovnymi jedne
c=eye(3)      % jednotkova matice 3x3

pause  %stiskni klavesu
clc

% KOMPLEXNI CISLA
% Promenne mohou byt komplexni. Zadavaji se pak obvyklou formou 
% mapr.  2+i*3,    kde symboly i a j  jsou vyhrazeny pro imag. jednotku.
% Priklad:

zc=2+j*3   % komplexni cislo

% jeho real. a imag. cast dostaneme nasledovne

zr=real(zc),zi=imag(zc)   

pause  %stiskni klavesu

% naopak absol. hodnotu a uhel dostaneme 

zabs=abs(zc),zr=angle(zc)  

% DULEZITE: Uhel je opet udavan v radianech. Prevod na stupne 
% umozni vypocet deg=rad*180/pi.

za= 180*angle(zc)/pi  %uhel je tentokrat ve stupnich

pause   %stiskni klavesu
clc

% Matici komplex. sdruzenou k matici jine dostaneme pomoci "conj"

zstar=conj(zc)  
pause  %stiskni klavesu
clc

% TRANSPOZICE MATICE
% K transpozici se pouziva znak apostrofu ('). Napr.

c=[1 5 2;3 4 8]   % matice 2x3

%po transpozici vypada takto
c'    

pause  %stiskni klavesu
clc

%DULEZITE: U komplexnich matic pouziti symbolu ' vytvori 
% matici transponovanou s prvky komplexne sdruzenymi. Napr.

a=[2+i -1+5*i;6 4+i*3]  % komlexni matice
b=a'      %po transpozici (vsimnete si znamenek u imag. casti)

pause  %stiskni klavesu

% Pokud bych chteli provest pouze traspozici bez sdruzeni, musime pouzit
% symbol .' (tecka a za ni apostrof) - viz priklad:

c=a.'  

pause  %stiskni klavesu
clc

% Vyzkousejme si dale tyto priklady
a1=[1 2 3 ; 5,6 7]      % matice 3x2
b=[1+2*i  5 -j*7 9]    % radkovy vektor o 4 prvcich (mezery!)
c=[2 4 6 8]'            % sloucovy vektor o 4 prvcich
pause  %stiskni klavesu

d=(2:2:8)'              % stejne jako vektor c
e=a1(:)                 % sloupcovy vektor o 6 prvcich

pause  %stiskni klavesu
clc


% OPERACE S MATICEMI
% Operace s maticemi jsou nejvykonnejsi slozkou MATLABu, ale zaroven
% pri nich muze vzniknout rada chyb (kvuli nespravnemu pochopeni).
% Operace si ukazeme na prikladech 2 matic  a, b.
a=[1 2;3 4],b=[4 5;6 7],
pause  %stiskni klavesu

% SCITANI A ODCITANI:
% Tyto operace jsou vykonavany beznym zpusobem, tj. prvek po prvku.
% Pri operacich, v nichz se vyskytuje skalar (konstanta) a matice,
% se skalar automaticky prevede na matici s odpovidajicimi rozmery a 
% se vsemi prvky rovnymi hodnote skalaru. Priklad:

a,c=4-a
pause  %stiskni klavesu

% Podminkou scitani ci odcitani dvou matic jsou jejcih shodne rozmery,
% jinak je hlasena chyba. Priklad.

a,b,d=a-b
pause  %stiskni klavesu
clc

% NASOBENI
% Operace a.*b (tecka a hvezdicka) predstavuje nasobeni prislusnych prvku
% matic. Matice a, b musi mit stejny rozmer, nebo jedna z nich musi 
% byt skalar.
a,b,c=a.*b
pause  %stiskni klavesu
clc

% Jestlize a je skalar, kazdy prvek b je vynasoben cislem a
a,d=4 .*a

%DULEZITE Vsimnete si v predchozim priklade, ze za cislici 4
% musi byt mezera, aby tecka nebyla brana jako desetinna tecka.

pause  %stiskni klavesu
clc

% Skutecny maticovy soucin se zapise jako a*b
% Musi samozrejme platit, pocet sloucu matice a musi souhlasit s poctem
% radku b, jinak je hlasena chyba. Pokud je a skalar, pak a*b = a.*b.

a,d=4;d1=d.*a,d2=d1*a
pause  %stiskni klavesu
clc

% Treti forma nasobeni jen tzv. vnejsi produkt matic (outter product)
% a to v pripade, ze a sloupcovy vektor a b je radkovy vektor.

a=[1;2;3],b=a',c=a*b
pause  %stiskni klavesu
clc

% DELENI
% Deleni jednotlivych prvku matic se oznacuje  a./b. 
% Bud a nebo b muze byt skalar.
 
a=[2 4;6 8],b=[1 2;2 4],c=a./b
pause  %stiskni klavesu
clc 

% Operace a/b jiz neni uplne samozrejma na pochopeni. 
% Navic existuji dva operatory deleni, leve deleni (symbol \)
% a prave deleni (/).
% Leve deleni A\B. Jestlize A je ctvercova matice, A\B je co do vysledku
% totez jako inv(A) * B. 
% Prave deleni A/B = B * inv(A).

pause  %stiskni klavesu
clc 

% EXPONENCIALNI OPERACE
% Opet existuje prvkova (.^) a maticova (^). Priklad

a=[1 2;3 4],b=a.^2,c=(2).^a,d=a^2

pause  %stiskni klavesu
clc 


% Dale existuji funkce sqrt a exp, ktere pracuji beznym zpusobem s prvky 
% matic.

a=[1 4;9 36],b=sqrt(a)
pause  %stiskni klavesu
clc 

% DALSI UZITECNE FUNKCE
% round(x): zaokrouhli x smerem k nejblizsimu celemu cislu
x=[2.1 3.6;-3.1 -3.6],y=round(x)

pause  %stiskni klavesu
clc 

%fix(x): odrizne desetinou cast

x=[2.1 3.6;-3.1 -3.6],y=fix(x)
pause  %stiskni klavesu
clc 

%floor(x), ceil(x): 'zaokrouhli' x smerem k nizsimu resp. vyssimu celemu cislu
x=[2.1 3.6;-3.1 -3.6],y=floor(x),z=ceil(x)
pause  %stiskni klavesu
clc 

%sign(x): vraci 1 kdyz x>0 nebo -1 kdyz x<0 a 0 kdyz x=0
x=[2.1 3.6;-3.1 0],y=sign(x)

pause  %stiskni klavesu
clc 

%rem(x,a): vraci zbytek po deleni cisla x cislem a
x=[2.1 35;-3 0],y=rem(x,3)

pause  %stiskni klavesu
clc 

% SLOUPCOVE OPERACE
%min(x),max(x): vraci minimum resp. maximum z kazdeho sloupce x.
x=[2.1 3.6 1.5;-3.1 1 2.3],ymin=min(x),ymax=max(x)
pause  %stiskni klavesu
clc 

%mean(x),median(x): vraci prumer ci median z kazdeho sloupce x
x=[2 4 3;-3 4 2;3 3 3],y=mean(x),z=median(x)
pause  %stiskni klavesu
clc 

%sort(x) vraci setridene sloupce x
x=[2 4 3;-3 4 2;-1 1 -2],y=sort(x)

%[y,k]=sort(x) navic vraci indexy x pouzite pri trideni

[y,k]=sort(x)
pause  %stiskni klavesu
clc 

%sum(x),prod(x) soucet resp. soucin sloupcu x
x=[2 4 3;-3 4 2;2 1 -1],y=sum(x),z=prod(x)
pause  %stiskni klavesu
clc 

%cumsum(x),cumprod(x) kumulativni soucet resp. soucin sloucu x
x=[2 4 3;-3 4 2;2 -2 3],y=cumsum(x),z=cumprod(x)
pause  %stiskni klavesu
clc 

%diff(x) rozdil mezi sousednimi hodnotami ve sloupcich x
x=[2 4 3;-3 4 2;3 -3 5],y=diff(x)

pause     %stiskni klavesu
clc

% POLYNOMY A KORENY

% Mejme polynom f(x) = x^3 + 5x^2  +3x + 2
% Chceme-li nalezt jeho koreny, vytvorime pole koeficientu
p=[1 5 3 2]
% musi byt v poradi smerem od vyssich k nizsim stupnum

% Pro nalezeni korenu zadame
rts=roots(p)
% Vysledekem je sloucovy vektor!

pause     %stiskni klavesu

% Naopak pro vytvoreni polynomu z jeho korenu pouzijeme prikaz "poly"
pp=poly(rts)
% Pole pp by melo byt shodne s p (v ramci presnosti pocitace)
[p;pp]

pause     %stiskni klavesu

% Pokud chceme realne koeficienty, pouzijeme
rts=real(poly(rts))

pause     %stiskni klavesu

% Chceme-li vypocitat polynom f(x) pro ruzne hodnoty x
% musime nejprve vygenerovat pole hodnot. Napr.
zz=[0 1 2 -1]

% Potom pouzijeme "polyval" pro vypocet funkcnich hodnot
ff=polyval(p,zz)

pause  %PO STISKU KLAVESY SKONCI TENTO TUTORIAL
echo off 
%clear a a1 avar b c d d1 d2 e ff i j k p pp rts s
%clear x y ymax ymin z za zabs zc zi zr zstar zz
clear
