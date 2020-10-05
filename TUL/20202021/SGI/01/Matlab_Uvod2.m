clc,echo on
%		PRACE S GRAFY

% MATLAB nabizi vynikajici moznosti, jak jednoduse a nazorne
% zobrazovat data

% Nejjednodusi moznost, jak zobrazit data ulozena ve vektoru
% je pouzit prikaz PLOT, priklad:

t = -50:50;
x = sin (t/2);
plot (x)

pause % stiskni klavesu

% predchozi jednoducha verze prikazu plot vykreslila vsechny body
% vektoru x, avsak neumoznuje uvest spravne poradnice na ose x
% abychom spavne zobrazili hodnoty x ve vztahu k promenne t,
% musime zadat 

plot (t, x)

pause % stiskni klavesu

% chceme-li vykreslit napr. jen cast dat, lze to jednoduse

plot (t(1:10),x(1:10))


pause % stiskni klavesu

% chceme-li vytvorit nekolik nezavislych obrazkovych oken, pouzijeme
% prikaz FIGURE (N), napr.

figure (1)
plot (t(1:10),x(1:10))

figure (2)
plot (t(11:20),x(11:20))

pause % stiskni klavesu

% chceme-li mit v jednom okne vce obrazku pouzijeme prikaz
% SUBPLOT (N,M, K), kde N a M jsou rozmery obrazkove 'matice'
% a K je poradove cislo obrazku, priklad:

clf % smaze stavajici okno


figure (1)
subplot (2,2,1)
plot (t(1:10),x(1:10))
subplot (2,2,2)
plot (t(11:20),x(11:20))
subplot (2,2,3)
plot (t(21:30),x(21:30))


pause % stiskni klavesu


% vice prubehu v jednom grafu lze ziskat pomoci vyctu
% nekolika parametru, napr:

clf reset
y= cos (t/4+2);
plot (t,x,'r-',t,y,'g--')


pause % stiskni klavesu

% pridat titulek a nazvy os lze snadno:

title ('Graf 1')
xlabel ('osa x')
ylabel ('osa y')


pause % stiskni klavesu

% zavrit okno se da takto

close (2)


pause % stiskni klavesu


% graficke moznosti jsou MATLABu jsou rozsahle, jako ukazku uvedme
% alespon jak snadno vykreslit 3D zavislosti

x= -8: 0.5:  8;
y = x;
[X,Y] = meshgrid(x,y);
R = sqrt(X.^2 + Y.^2) + eps;
Z = sin(R)./R;
mesh (Z);

pause % TOTO JE KONEC UKAZKY



