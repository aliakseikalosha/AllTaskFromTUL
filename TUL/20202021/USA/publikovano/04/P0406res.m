clear all
clc

%ad a
a=binopdf(10,25,0.49)

%ad b
%tj. 10, 11, 12 ...
b=1-binocdf(9.5,25,0.49)

%ad c
c=1-binocdf(15.5,25,0.49)

%ad d
d=0;
pravd=0;
for i=0:25
    p=binopdf(i,25,0.49);
    if p>pravd
        pravd=p;
        d=i;
    end
end
d

%graf
x=0:1:25
for i=0:25
    y(i+1)=binopdf(i,25,0.49);
end
y
plot(x,y,'b+ ')