x=[1,2,3,4,5,6,6.5];
y=[3,5.1,6.9,8.8,10.9,13.3,14.1];
LM1=LinearModel.fit(x,y,'quadratic')
LM2=LinearModel.fit(x,y)

%z výsledkù získaného parametru 0.0322 a smìrodatné odchylky 0.0194 
%se vypoète intervalový odhad 

amin=0.0322-0.0194.*tinv(0.975,5)
amax=0.0322+0.0194.*tinv(0.975,5)
