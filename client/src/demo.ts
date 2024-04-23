//let data: string | number;
let data: any
data = '42';
data = 10;

interface ICar {
    color: string;
    model: string;
    topspeed?: number;
}

const car1: ICar = {
    color: 'blue',
    model: 'bmw'
};

const car2: ICar = {
    color: 'Grey',
    model: 'mercedez',
    topspeed: 120
};

const multiply = (x: number, y: number): number => {
    return x * y;
};

const multiplyy = (x: number, y: number): string => {
    return (x * y).toString();
};