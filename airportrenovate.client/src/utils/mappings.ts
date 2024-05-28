// 對應狀態碼到中文
export const StatusMapping: { [key: string]: string } = {
    "": "無",
    "A": "土木",
    "B": "水電",
    "C": "建築",
    "D": "綜合",
    "E": "機械"
};

export const ReverseStatusMapping: { [key: string]: string } = {
    "無": "",
    "土木": "A",
    "水電": "B",
    "建築": "C",
    "綜合": "D",
    "機械": "E"
};

export const AuthMapping: { [key: string]: string } = {
    "A": "工務組",
    "B": "業務組",
    "C": "人事室",
    "D": "中控室",
    "E": "北竿站",
    "F": "企劃組",
    "G": "南竿站",
    "H": "政風室",
    "I": "航務組",
    "J": "總務組"
}

export const ReverseAuthMapping: { [key: string]: string } = {
    "工務組": "A",
    "業務組": "B",
    "人事室": "C",
    "中控室": "D",
    "北竿站": "E",
    "企劃組": "F",
    "南竿站": "G",
    "政風室": "H",
    "航務組": "I",
    "總務組": "J"
}