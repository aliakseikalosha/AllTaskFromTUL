% vzorec pro v�po�et
%(M nad m)*(N-M nad n-m)/(N nad n)

%celkem lahv�           N=10
%vylosov�no             n=3
%perliv�ch              M=6
%vybr�no perliv�ch      m=voliteln�

a=nchoosek(6,3)*nchoosek(10-6,3-3)/nchoosek(10,3)
b=nchoosek(6,2)*nchoosek(10-6,3-2)/nchoosek(10,3)

d=0;
for i=0:3
    d=d+nchoosek(6,i)*nchoosek(10-6,3-i)/nchoosek(10,3);
end
d
    