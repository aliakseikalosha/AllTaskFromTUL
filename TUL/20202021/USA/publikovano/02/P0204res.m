% vzorec pro výpoèet
%(M nad m)*(N-M nad n-m)/(N nad n)

%celkem lahví           N=10
%vylosováno             n=3
%perlivých              M=6
%vybráno perlivých      m=volitelné

%a)
%první souèin je ze šestice perlivých chceme vybrat trojici
%druhý souèin je ze ètyø neperlivých chceme vybrat právì 0
%jmenovatel z deseti celkem chceme vybrat trojici
a=nchoosek(6,3)*nchoosek(10-6,3-3)/nchoosek(10,3)

%b)
b=nchoosek(6,1)*nchoosek(10-6,3-1)/nchoosek(10,3)

%d)
d=0
for i=0:3
    d(i+1)=nchoosek(6,i)*nchoosek(10-6,3-i)/nchoosek(10,3);
end
d
    