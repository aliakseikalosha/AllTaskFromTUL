x=0:1:25;
for i=0:25
    y(i+1)=binopdf(i,25,0.49);
end
vysledek_a=binopdf(10,25,0.49)
vysledek_b=1-binocdf(9.5,25,0.49)
vysledek_c=1-binocdf(15.5,25,0.49)

pravdepodobnost=0;
for i=1:26
    if y(i)>pravdepodobnost
        pravdepodobnost=y(i);
        vysledek_d=i-1;
    end
end
vysledek_d

plot(x,y,'b+ ')
