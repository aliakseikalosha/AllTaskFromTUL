clear all
clc

%vykreslí graf, který rozdělí oblast kde se potkají a kde již ne. 
%vodorovná osa příchod prvního
%svislá osa příchod druhého
%diagonála přijdou ve stejný čas
x=linspace(15,60);
y=linspace(0,45);

hold on
plot(x,y)
plot(y,x)
xlabel('prichod prvniho v [min]');
ylabel('prichod druheho v [min]');
hold off

P=1-(45*45)/(60*60)
