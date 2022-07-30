import {User} from "./User.model";
import {Project} from "./Project.model";

export interface Team{
  id: number,
  name: string,
  createdAt: Date,
  users?: User[],
  projects?: Project[]
}
