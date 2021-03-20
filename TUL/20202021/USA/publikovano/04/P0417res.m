clear all
clc

%intenzita lambda je 0.5 poruch za rok a stroj
%délka sledovani je 2 roky
%pocet strojù je 25
%t = 50
%Celkovì lze oèekávat za 2 roky na 25 strojích 25 poruch

vysledek_a=poisscdf(11.5,0.5*2*25)
vysledek_b=poisspdf(20,0.5*2*25)
vysledek_c=1-poisscdf(25.5,0.5*2*25)