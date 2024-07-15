export interface Employee {
  employeeId: string;
  gid: string;
  fullName: string;
  email: string;
  password: string;
  isActive: boolean;
  managerId: string | null;
  empCreatedBy: string;
  empCreatedOn: Date;
  managerName: string | null;
  empCreatedByName: string;
}