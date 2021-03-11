clear all
clc

syms x a b

f=1/(b-a);
strhod=int(x.*f,a,b)
rozptyl=int((x-strhod).^2.*f,a,b)
smerodch=sqrt(rozptyl)

tretimoment=int((x-strhod).^3.*f,a,b);
ctvrtymoment=int((x-strhod).^4.*f,a,b);

sikmost=tretimoment./smerodch.^3
spicatost=ctvrtymoment./smerodch.^4