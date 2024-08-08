export class Message {
    title: string = "";
    value: string = "";
    status: string = "";

    static getString(message: Message): string {
        return `Title: ${message.title}\r\nStatus: ${message.status}\r\nMessage: ${message.value}`;
    }
}