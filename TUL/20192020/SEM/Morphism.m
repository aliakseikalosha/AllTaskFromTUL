function [xx,yy] = Morphism(B,x,y,a)
X = [x;y];
Y = B*X;
xx = Y(1,:);
yy = Y(2,:);
plot(xx,yy)
axis(a)
end

