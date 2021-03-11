clear all
clc

x=0:1:10;
for i=0:10
    y(i+1)=nchoosek(10,i)*0.1^i*0.9^(10-i);
end
format long
y

%Kreslení pravdìpodobnostní funkce
plot(x,y,'bx','MarkerSize',10)
title('pravdìpodobnostní funkce náhodného jevu');
xlabel('výsledek náhodného jevu');
ylabel('pravdìpodobnost');

