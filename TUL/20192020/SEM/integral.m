function z = integral(x,y)
z = 0;
for i = 1:(length(x)-1)
    z= z + (x(i+1)-x(i))/2*(y(i+1)+y(i));
end
end

