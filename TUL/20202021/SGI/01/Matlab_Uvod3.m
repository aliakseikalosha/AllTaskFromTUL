 clc,echo on
%		VYRAZY A PRACE S FUNKCEMI

% MATLAB dava moznost pracovat s funkcemi a s vyrazy, ktere jsou 
% definovany jako textove retezce.

% Chceme-li definovat y=4*x+3 jako textovy vyraz musime pouzit
% apostrofy na obou stranach.

y='4*x+3'

echo on

% Vidime, ze MATLAB odpovedel vypsanim retezce.

pause  %Stiskni klavesu

% Chceme-li nyni vyse uevedeny vyraz vyhodnotit napr. 
% pro x=0, 2, 3 a 5, napiseme
x=[0 2 3 5]  
z=eval(y)    

pause  %Stiskni klavesu
clc

%	DEFINICE FUNKCI V M-SOUBORECH (M-FILE)

% MATLAB umoznuje pouzivat nejen vestavene funkce 
% ale tez definovat si vlastni v tzv. m-souborech .
% Takovy soubor se pak oznacuje jako FUNCTION M-FILE.

% Jako priklad si uvedeme soubor pro definici funkce sinc(x) 
% dane jako sin(pi*x)/(pi*x) v souboru sinc.m
% Chceme-li videt jeho obsah, napiseme sinc (bez pripony).

pause %Stiskni klavesu

echo off
disp('function y = sinc(x)')
disp('% SINC Sinc Function.')
disp('%')
disp('%      Y=SINC(X) generates sin(pi*x)/(pi*x)')
disp(' ')
disp('x=pi*x;i=find(x==0);[m,n]=size(i);x(i)=ones(m,n);')
disp('y=sin(x)./x;y(i)=ones(m,n);')
echo on


% POPIS: Prvni radka uvadi definici funkce a argumentu.
% Radky za % jsou komentare, ktere se objevi po napsani HELP SINC.


pause  %Stiskni klavesu

% Napr., pro t=-2:0.5:1; muzeme dat vypocitat z=sinc(t+1) takto
t=-2:0.5:1
z=sinc(t+2)

% Soubory s funkcemi nam dovoluji dale rozsirovat MATLAB.
% Nakonec vetsina toolboxu matlabu jsou vlastne m-soubory.

pause  %Stiskni klavesu
clc

%		VYHODNOCOVANI FUNKCI

% Funkce v m-souborech muzeme snadno vyhodnocovat prikazem "feval"
% nasledovne

f=0:.25:1
p=feval('sinc',f)

% Vsimnete si apostrofu u jmena funkce.


pause  %Stiskni klavesu

clc

%		DAVKOVE M-SOUBORY (SCRIPT M-FILES)

% Davkove soubory jsou sekvence prikazu MATLABu ulozene v souborech.
 

% Vypsanim jmena takoveho souboru se spusti vykonavani jednotlivych 
% prikazu.


%		DENIK (DIARY)

% Vsechny prikazy provadene v ramci jednoho spusteni MATLAB mohou byt
% ulozeny do 'deniku'. Napr.
diary denik.m
x=0:.2:1

% Vsechny prikazy a zobrazene texty budou nyni ukladany 
% do daneho souboru dokud nevydame pokyn zavrit denik:
diary off

pause  %Stiskni klavesu

% Zde si muzeme prohlednout obsah souboru denik:


type denik.m

pause  %Stiskni klavesu

% Tento soubor muze byt - po patricne editaci - pouzit jako 
% davkovy m-soubor.

pause  %STISKEM KLAVESY SKONCI TUTO SEKCI

clc,echo off
clear
