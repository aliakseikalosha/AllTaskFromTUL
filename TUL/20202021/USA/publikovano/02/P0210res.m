clear all
clc

%pravdìpodobnost, že první koule bude èervená
P1=4/12;
%nyní mùžeme ukonèit víme, že døíve byla vytažena èervená

%pravdìpodobnost, že první bude èerná, druhá èervená
P2=(5/12)*(4/11);
%nyní mùžeme ukonèit víme, že døíve byla vytažena èervená

%pravdìpodobnost, že první dvì budou èerné, tøetí èervená
P3=(5/12)*(4/11)*(4/10);

%pravdìpodobnost, že první tøi budou èerné, ètvrtá èervená
P4=(5/12)*(4/11)*(3/10)*(4/9);

%pravdìpodobnost, že první ètyøi budou èerné, pátá èervená
P5=(5/12)*(4/11)*(3/10)*(2/9)*(4/8);

%pravdìpodobnost, že prvních pìt bude èerných, šestá èervená
P6=(5/12)*(4/11)*(3/10)*(2/9)*(1/8)*(4/7);
P=P1+P2+P3+P4+P5+P6
