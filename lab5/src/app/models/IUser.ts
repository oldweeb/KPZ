import IGroup from "./IGroup";

interface IUser {
  id: string;
  firstName?: string;
  lastName: string;
  middleName?: string;
  position: Position;
  email: string;
  group: IGroup;
  password: string;
}

export enum Position {
  Professor,
  Assistant,
  Student,
  SystemAdministrator
}

export default IUser;