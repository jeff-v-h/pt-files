import React from 'react';
import { Switch, Route } from 'react-router-dom';
import Dashboard from './components/dashboard/Dashboard';
import PatientPage from './components/patient/PatientPage';
import CasefilesPage from './components/casefiles/CasefilesPage';
import ConsultationPage from './components/consultation/ConsultationPage';
import ConsultationsPage from './components/consultations/ConsultationsPage';
import PatientsPage from './components/patients/PatientsPage';
import Casefile from './components/casefile/Casefile';

export default class Routes extends React.Component {
  render() {
    return (
      <Switch>
        <Route
          path="/patients/:patientId/casefiles/:casefileId/consultations/:consultId"
          component={ConsultationPage}
        />
        <Route
          path="/patients/:patientId/casefiles/:casefileId/consultations"
          component={ConsultationsPage}
        />
        <Route path="/patients/:patientId/casefiles/:casefileId" component={Casefile} />
        <Route path="/patients/:patientId/casefiles" component={CasefilesPage} />
        <Route path="/patients/:patientId" component={PatientPage} />
        <Route exact path={['/', '/patients']} component={PatientsPage} />
      </Switch>
    );
  }
}
