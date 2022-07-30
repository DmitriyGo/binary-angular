import {Project} from "./Project.model";
import {User} from "./User.model";

export interface ProjTask{
  id: number,
  projectId: number,
  project?: Project,
  performerId: number,
  performer?: User,
  name: string,
  description: string,
  state: 3,
  createdAt: Date,
  finishedAt: Date
}
