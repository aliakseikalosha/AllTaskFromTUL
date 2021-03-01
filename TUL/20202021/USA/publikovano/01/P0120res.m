clear all
clc

%uèitel vybere z 10 pøíkladù 3. Jedná se o kombinace, nezáleží na poøadí
%jak je zadává
kombinace_ucitel=nchoosek(10,3);

%a)
%studenti znají 5 pøíkladù z nichž budou vybrány 3.
kombinace_studenti=nchoosek(5,3);

%pravdìpodobnost
%poèet úspìšných jevù - studenti z 5 znají všechny 3 pøíklady
%poèet všech jevù - uèitel má k dispozici 10 a použije 3 pøíklady

Pa=kombinace_studenti/kombinace_ucitel

%b)
%studenti znají 5 pøíkladù z nichž jsou vybrány 2
kombinace_studenti=nchoosek(5,2)
%studenti neznají 5 pøíkladù z nichž je vybrán 1
kombinace_studenti_neznaji=nchoosek(5,1)

%pravdìpodobnost
Pb=kombinace_studenti*kombinace_studenti_neznaji/kombinace_ucitel
