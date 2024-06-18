// 對應狀態碼到中文
export const Status3Mapping: { [key: string]: string } = {
    "": "無",
    "A": "土木",
    "B": "水電",
    "C": "建築",
    "D": "綜合",
    "E": "機械"
};

//export const Status3Mapping: { text: string, value: string }[] = [
//    { text:"", value: "無" },
//    { text:"A", value: "土木" },
//    { text:"B", value: "水電"},
//    { text:"C", value: "建築" },
//    { text:"D", value: "綜合"},
//    { text:"E", value: "機械" }
//];

//export const ReverseStatusMapping: { [key: string]: string } = { // 轉換資料後存進資料庫
//    "無": "",
//    "土木": "A",
//    "水電": "B",
//    "建築": "C",
//    "綜合": "D",
//    "機械": "E"
//};
export const ReverseStatusMapping: { text: string, value: string }[] = [ // 轉換資料後存進資料庫
    { text: "無", value: "" },
    { text: "土木", value: "A"},
    { text: "水電", value: "B"},
    { text: "建築", value: "C"},
    { text: "綜合", value: "D"},
    { text: "機械", value: "E"}
];
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

//export const ReverseAuthMapping: { [key: string]: string } = { // 轉換資料後存進資料庫
//    "工務組": "A",
//    "業務組": "B",
//    "人事室": "C",
//    "中控室": "D",
//    "北竿站": "E",
//    "企劃組": "F",
//    "南竿站": "G",
//    "政風室": "H",
//    "航務組": "I",
//    "總務組": "J"
//}

export const ReverseAuthMapping: { text: string, value: string }[] = [
    { text: '工務組', value: 'A' },
    { text: '業務組', value: 'B' },
    { text: '人事室', value: 'C' },
    { text: '中控室', value: 'D' },
    { text: '北竿站', value: 'E' },
    { text: '企劃組', value: 'F' },
    { text: '南竿站', value: 'G' },
    { text: '政風室', value: 'H' },
    { text: '航務組', value: 'I' },
    { text: '總務組', value: 'J' }
];
