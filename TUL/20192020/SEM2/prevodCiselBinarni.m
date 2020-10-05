clear all;
clc;

i=1;
a=13.25;
celacast=a/1-mod(a,1);
necela=a-celacast;
while (celacast >= 1)%2^n
    if(mod(celacast,2) == 0)
        A(i)=0;
    else
        A(i)=1;
    end
    celacast=celacast/2 - (mod(celacast,2)/2);
    i=i+1;
end
A
j=1;
poc=10;
while (poc > 0 & necela > 0)%1/2^n
    if(necela*2 < 1)
        B(j) = 0;
    else
        B(j)=1;
        necela = necela - 1;
    end
    necela=necela*2;
    j=j+1;
    poc=poc-1;
end
B
C=[A "." B]
