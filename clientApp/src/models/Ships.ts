export class Ship5 implements Ship {
    length: number = 5;
}

export class Ship4 implements Ship {
    length: number = 4;
}

export class Ship3 implements Ship {
    length: number = 3;
}

interface Ship {
    length: number;
}