clear all
clc

%variace bez opakování, jedná se o dvojprvkové množiny
%zalezi na poøadí, když první odešle druhému, oèekává, že i druhý pošle
%pohlednici prvnímu. 
pratel=8;
mnoziny=2;

vysledek=factorial(pratel)/factorial(pratel-mnoziny)
