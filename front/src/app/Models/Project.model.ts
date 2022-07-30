import {ProjTask} from "./ProjTask.model";
import {User} from "./User.model";
import {Team} from "./Team.model";

export interface Project{
  id: number,
  authorId: number,
  author?: User
  teamId: number,
  team?: Team,
  name: string,
  description: string,
  deadLine: Date,
  createdAt: Date,
  tasks?: ProjTask[]
}
