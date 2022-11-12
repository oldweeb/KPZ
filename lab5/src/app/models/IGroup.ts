import IUser from "./IUser";

interface IGroup {
  id: string;
  students: IUser[];
  name: string;
}

export default IGroup;