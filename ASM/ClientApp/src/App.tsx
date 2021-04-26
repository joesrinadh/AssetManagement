import { Route } from 'react-router';
import Layout from './components/Layout';
import { Assets } from './components/assets/Assets';
import { Asset } from './components/asset/Asset';
import { UploadAsset } from './components/upload/UploadAsset';
import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Assets} />
        <Route path='/asset/:assetId' component={Asset} />
        <Route path='/UploadAsset' component={UploadAsset} />
    </Layout>
);
