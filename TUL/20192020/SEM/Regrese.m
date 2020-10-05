function a = Regrese(x,y,s)
s1 = size(x);if s1(1) == 1 x = x'; end
s1 = size(y);if s1(1) == 1 y = y'; end
V = x.^0;
for i= 1:s
    V = [V,x.^i];
end
a= V\y;
end

