clear all
clc

%ad a
%nezáleží kolik bude es, králů atd. Jsou dvě skupiny, proto binomické
%rozdělení
pravd_a=binopdf(8,10,16/32)

%ad b
%musí být právě 2 esa, 3 krále, 2 filci, 1 spodek a 2 ostatní. Více než 2
%skupiny, proto multinomické rozdělení
pravd_b=mnpdf([2,3,2,1,2],[4/32,4/32,4/32,4/32,16/32])

%ad ca
%hypergeometrické rozdělení. Jsou dvě skupiny
pravd_ca=hygepdf(8,32,16,10)

%ad cb
pravd_cb=nchoosek(4,2)*nchoosek(4,3)*nchoosek(4,2)*nchoosek(4,1)*nchoosek(16,2)/nchoosek(32,10)
